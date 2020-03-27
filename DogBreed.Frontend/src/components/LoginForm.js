import { Form } from "mobx-react-form";
import dvr from "mobx-react-form/lib/validators/DVR";
import Validator from "validatorjs";

export default class LoginForm extends Form {
  constructor(dogBreedStore) {
    super();
    this.dogBreedStore = dogBreedStore;
  }
  plugins() {
    return {
      dvr: dvr(Validator)
    };
  }
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

  hooks() {
    return {
      onSuccess(form) {
        const data = form.values();
        this.dogBreedStore.login(data.email, data.password);
      },
      onError(form) {}
    };
  }
}
