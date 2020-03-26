import { Form } from "mobx-react-form";
import dvr from "mobx-react-form/lib/validators/DVR";
import Validator from "validatorjs";
import { inject } from "mobx-react";
import { DogBreedAdapter } from "../adapters/DogBreedAdapter";
import { RootStore } from "../shared/stores/RootStore";

class RegistrationForm extends Form {
  constructor(rootStore) {
    super();
    this.rootStore = rootStore;
    console.log("form", rootStore);
  }
  /*
    Below we are returning a `plugins` object using the `validatorjs` package
    to enable `DVR` functionalities (Declarative Validation Rules).
  */
  plugins() {
    return {
      dvr: dvr(Validator)
    };
  }
  /*
    Return the `fields` as a collection into the `setup()` method
    with a `rules` property for the validation.
  */
  setup() {
    return {
      fields: [
        {
          name: "email",
          label: "Email",
          placeholder: "Insert Email",
          rules: "required|email|string|between:5,25"
        },
        {
          name: "password",
          label: "Password",
          placeholder: "Insert Password",
          rules: "required|string|between:5,25"
        },
        {
          name: "passwordConfirm",
          label: "Password Confirmation",
          placeholder: "Confirm Password",
          rules: "required|string|same:password"
        }
      ]
    };
  }
  hooks() {
    return {
      onSuccess(form) {
        const data = form.values();
        const adapter = new DogBreedAdapter();
        let rootStore = new RootStore();

        adapter.registration(data.email, data.password);
        console.log("test", rootStore.routerStore.goTo("dogbreed"));
        rootStore.routerStore.goTo("dogbreed");
      },
      onError(form) {
        alert("Form has errors!");
        console.log("All form errors", form.errors());
      }
    };
  }
}

export default RegistrationForm;
