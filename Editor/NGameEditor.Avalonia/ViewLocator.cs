using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using NGameEditor.ViewModels;

namespace NGameEditor.Avalonia;



public interface IView<T> where T : class;



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
			.Select(
				x =>
				(
					viewType: x,
					viewModelType: GetViewModelType(x)
				)
			)
			.Where(x => x.viewModelType != null)
			.ToDictionary(x => x.viewModelType!, x => x.viewType);


	private static Type? GetViewModelType(Type viewType) =>
		viewType
			.GetInterfaces()
			.Where(x =>
				x.IsGenericType &&
				(
					x.GetGenericTypeDefinition() == typeof(IViewFor<>) ||
					x.GetGenericTypeDefinition() == typeof(IView<>)
				)
			)
			.Select(x => x.GenericTypeArguments.First())
			.FirstOrDefault();
}
