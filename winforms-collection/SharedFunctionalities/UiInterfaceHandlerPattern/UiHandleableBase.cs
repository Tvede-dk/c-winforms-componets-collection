using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.UiInterfaceHandlerPattern {
    /// <summary>
    /// intention: to be able to pair a ui with an object of a required / given type.
    /// </summary>
    public interface UiHandleableInterface {
        /// <summary>
        /// gets the type name of the matching object.
        /// </summary>
        /// <returns>the same name as the UIObjectHandleableInterface would return.</returns>
        string getMatchingTypeName();
    }

    /// <summary>
    /// intention: to be able to pair this object with a given UI. eg the UIHandleableInterface
    /// </summary>
    public interface UiObjectHandleableInterface {
        /// <summary>
        /// Gets the "typename" for this (to be paired with a ui).
        /// </summary>
        /// <returns>the name of the type of this object. can be the default typesystem's name, or a hardcoded provided one. </returns>
        string getMatchingTypeName();
    }
}
