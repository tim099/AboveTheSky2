using System;
using System.IO;
using UnityEngine;

public class UFTFileUtil
{
	// this function looks to the home folder, and if filemask=atlas
	// it will return atlas0 if no atlas exists, return atlas5 if ther is alreadr atlas0-atlas4 files in folder
	// this function will ignore any extension
	public static string getFileName(string fileMask){
		int maxId=-1;
		string[] files=Directory.GetFiles(@Application.dataPath,fileMask+"*");		
		foreach (string file in files){
			string fileName=file.Substring(file.LastIndexOf('/')+1);
			
			int lastDOT=fileName.LastIndexOf('.');
			string noExtension=fileName;
			if (lastDOT>-1){
				noExtension=fileName.Substring(0,lastDOT);
			} 
			string idString=noExtension.Replace(fileMask,"");
			int id=0;
			try{
				id=Convert.ToInt32(idString);
			}catch {}
			
			maxId=id>maxId? id:maxId;
			
		}
		
		if (maxId==-1){
			return fileMask+"0";
		} else {
			return fileMask+(++maxId);
		}
	}
}


