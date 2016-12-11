using System;
using System.Collections.Generic;

namespace SharedFunctionalities.UiInterfaceHandlerPattern {
    public class UiToObjectHandler<T> where T : IUiObjectHandleableInterface {
        private readonly Dictionary<string, Func<T, IUiHandleableInterface>> _typeToUi = new Dictionary<string, Func<T, IUiHandleableInterface>>();

        public UiToObjectHandler<T> AddUiForType(string typename, Func<T, IUiHandleableInterface> constructorFunction) {
            _typeToUi.Add(typename, constructorFunction);
            return this;
        }

        /// <summary>
        /// creates a ui from an
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>null if unable to pair.</returns>
        public IUiHandleableInterface CreateUiForObject(T obj) {
            return _typeToUi.ContainsKey(obj.GetMatchingTypeName()) ? _typeToUi[obj.GetMatchingTypeName()](obj) : null;
        } 


    }
}
