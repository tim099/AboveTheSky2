using System;


public class TextureDoesNotExistsException:Exception
{
	public string texturePath;
	public TextureDoesNotExistsException (string path)
	{
		texturePath=path;
	}
}


