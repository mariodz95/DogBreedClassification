import { Form } from "mobx-react-form";
import dvr from "mobx-react-form/lib/validators/DVR";
import Validator from "validatorjs";
import { DogBreedAdapter } from "../adapters/DogBreedAdapter";
import { RootStore } from "../shared/stores/RootStore";

export default class LoginForm extends Form {
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
        }
      ]
    };
  }

  /*
    Event Hooks
  */
  hooks() {
    return {
      /*
        Success Validation Hook
      */
      onSuccess(form) {
        alert("Form is valid! Send the request here.");
        const data = form.values();
        const adapter = new DogBreedAdapter();
        adapter.login(data.email, data.password);
        // get field values
        let rootStore = new RootStore();

        adapter.registration(data.email, data.password);
        console.log("test", rootStore.routerStore.goTo("dogbreed"));
        rootStore.routerStore.goTo("dogbreed");
        console.log("Form Values!", form.values());
      },
      /*
        Error Validation Hook
      */
      onError(form) {
        alert("Form has errors!");
        // get all form errors
        console.log("All form errors", form.errors());
      }
    };
  }
}
