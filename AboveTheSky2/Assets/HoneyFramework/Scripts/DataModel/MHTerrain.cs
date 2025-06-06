using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HoneyFramework
{
    /*
     *  Terrain definition class used for serializing all terrain settings which constructs single hex
     */
    public class MHTerrain : MHType
    {
        static public List<MHTerrain> list = new List<MHTerrain>();

        public enum Mode
        {
            normal,
            IsBorderType,
            IsRiverType,
            DoNotUseThisTerrain,
        }

        public enum Type
        {            
            Plains,
            Hills,
            Mountains,
            Steppe,
            Snow,
            Sea,
        }
        
        public Mode mode = Mode.normal;
        public List<Type> typeList = new List<Type>();

        // note this field is exposed as it is special. 
        // during terrain generation we will use its value to build beach areas. 
        // Note as well that it is not a requirement to have sea type as border type or have them at all
        public bool seaType
        {
            get { return typeList.Contains(Type.Sea); }
        }

        public string diffusePath = String.Empty;
        public string heightPath = String.Empty;
        public string mixerPath = String.Empty;
        
        public List<MHSimpleCounter> fgTypes = new List<MHSimpleCounter>();
        public string foregroundColor = "71792f";

        /// <summary>
        /// Checks if terrain name is unique, by default it will acquire name from diffuse texture
        /// </summary>
        /// <returns></returns>
        public bool IsNameUnique()
        {            
            if (name.Length == 0)
            {
                return false;
            }
            List<MHTerrain> names = list.FindAll(o => ((o.name == name) && o != this));
            return names.Count == 0;
        }

        /// <summary>
        /// Use name of the diffuse texture if no name is found for terrain definition
        /// </summary>
        /// <returns></returns>
        public void UseDiffusenameIfNoName()
        {
            if(name.Length == 0)
            {
                int index = diffusePath.LastIndexOf("/");
                if (index > 0)
                {
                    name = diffusePath.Substring(index+1);
                }
                else
                {
                    name = diffusePath;
                }
            }            
        }
    }
}
