using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_collection.validator {

	public interface IValidatorType {
		bool Validate(String text);
		string getErrorMessage();
	}
}
