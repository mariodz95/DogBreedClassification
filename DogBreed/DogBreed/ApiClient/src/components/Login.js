import React from "react";
import LoginForm from "./LoginForm";
import { observer, inject } from "mobx-react";

const form = new LoginForm();

@inject("rootStore")
@observer
class Login extends React.Component {
  render() {
    return (
      <React.Fragment>
        {/* <form>
          <label>{form.$("email").label}</label>
          <input {...form.$("email").bind()} />
          <p>{form.$("email").error}</p>

          <label htmlFor={form.$("password")}>{form.$("password").label}</label>
          <input {...form.$("password").bind()} />
          <p>{form.$("password").error}</p> */}

        <button type="submit" onClick={this.handleClick}>
          Submit
        </button>
        {/* <button type="button" onClick={form.onClear}>
            Clear
          </button>
          <button type="button" onClick={form.onReset}>
            Reset
          </button>

          <p>{form.error}</p> */}
        {/* </form> */}
      </React.Fragment>
    );
  }
  handleClick = () => {
    const { rootStore } = this.props;
    const result = rootStore.dogBreedStore.login(
      "mariodz95@gmail.com",
      "test123"
    );
    console.log("handle clikc", result);

    console.log("handle clikc promise", result.promise);
    if (result.promise) {
      console.log("handle clikc result", result);

      rootStore.routerStore.goTo("dogbreed");
    }
  };
}

export default Login;
