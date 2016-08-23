using System;

namespace winforms_collection.validator {

    public interface IValidatorType {
		bool Validate(String text);
		string getErrorMessage();
	}
}
