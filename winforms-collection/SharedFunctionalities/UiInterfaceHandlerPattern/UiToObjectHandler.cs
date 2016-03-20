using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.UiInterfaceHandlerPattern {
    public class UiToObjectHandler<T> where T : UiObjectHandleableInterface {
        private Dictionary<string, Func<T, UiHandleableInterface>> typeToUi = new Dictionary<string, Func<T, UiHandleableInterface>>();

        public UiToObjectHandler<T> AddUiForType(string typename, Func<T, UiHandleableInterface> constructorFunction) {
            typeToUi.Add(typename, constructorFunction);
            return this;
        }

        /// <summary>
        /// creates a ui from an
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>null if unable to pair.</returns>
        public UiHandleableInterface createUiForObject(T obj) {
            return typeToUi.ContainsKey(obj.getMatchingTypeName()) ? typeToUi[obj.getMatchingTypeName()](obj) : null;
        } 


    }
}
