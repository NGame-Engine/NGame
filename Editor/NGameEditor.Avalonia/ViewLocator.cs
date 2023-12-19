using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using NGameEditor.ViewModels;

namespace NGameEditor.Avalonia;



public class ViewLocator : IDataTemplate
{
	private static readonly Dictionary<Type, Type> ViewsByViewModel =
		GetViewsByViewModel(
			typeof(ViewLocator).Assembly,
			typeof(ViewModelBase).Assembly
		);


	public Control Build(object? data)
	{
		var viewModelType = data!.GetType();
		if (ViewsByViewModel.TryGetValue(viewModelType, out var viewType))
		{
			return (Control)Activator.CreateInstance(viewType)!;
		}


		return new TextBlock { Text = "Not Found: " + viewModelType.FullName };
	}


	public bool Match(object? data)
	{
		return data is ViewModelBase;
	}


	private static Dictionary<Type, Type> GetViewsByViewModel(params Assembly[] assemblies) =>
		assemblies
			.SelectMany(x => x.GetTypes())
			.Where(x =>
				x.GetInterfaces()
					.Any(
						i =>
							i.IsGenericType &&
							i.GetGenericTypeDefinition() == typeof(IViewFor<>)
					)
			)
			.ToDictionary(
				viewType =>
					viewType
						.GetInterfaces()
						.Where(x =>
							x.IsGenericType &&
							x.GetGenericTypeDefinition() == typeof(IViewFor<>)
						)
						.Select(x => x.GenericTypeArguments.First())
						.First(),
				viewType => viewType
			);
}
