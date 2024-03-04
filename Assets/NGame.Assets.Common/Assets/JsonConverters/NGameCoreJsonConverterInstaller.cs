using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Assets.JsonConverters.SystemDrawing;

namespace NGame.Assets.Common.Assets.JsonConverters;



public static class NGameCoreJsonConverterInstaller
{
	public static IHostApplicationBuilder AddNGameCoreJsonConverters(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<JsonConverter, SemVersionConverter>();

		builder.Services.AddTransient<JsonConverter, ColorJsonConverter>();
		builder.Services.AddTransient<JsonConverter, PointFJsonConverter>();
		builder.Services.AddTransient<JsonConverter, PointJsonConverter>();
		builder.Services.AddTransient<JsonConverter, RectangleFJsonConverter>();
		builder.Services.AddTransient<JsonConverter, RectangleJsonConverter>();
		builder.Services.AddTransient<JsonConverter, SizeFJsonConverter>();
		builder.Services.AddTransient<JsonConverter, SizeJsonConverter>();

		return builder;
	}
}
