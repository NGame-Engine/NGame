using System.Text.Json;

namespace NGameEditor.Backend.Configurations.UserDatas;



public interface IUserDataSerializer
{
	UserData Deserialize(string json);
	string Serialize(UserData userData);
}



public class UserDataSerializer : IUserDataSerializer
{
	public UserData Deserialize(string json)
	{
		var jsonUserData = JsonSerializer.Deserialize<JsonUserData>(json)!;

		return new UserData
		{
			LastOpenedScene = jsonUserData.LastOpenedScene
		};
	}


	public string Serialize(UserData userData)
	{
		var jsonUserData = new JsonUserData
		{
			LastOpenedScene = userData.LastOpenedScene
		};

		return JsonSerializer.Serialize(jsonUserData);
	}
}
