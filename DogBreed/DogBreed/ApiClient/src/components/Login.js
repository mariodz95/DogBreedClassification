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
        <form>
          <label>{form.$("email").label}</label>
          <input {...form.$("email").bind()} />
          <p>{form.$("email").error}</p>

          <label htmlFor={form.$("password")}>{form.$("password").label}</label>
          <input {...form.$("password").bind()} />
          <p>{form.$("password").error}</p>

          <button type="submit" onClick={form.onSubmit}>
            Submit
          </button>
          <button type="button" onClick={form.onClear}>
            Clear
          </button>
          <button type="button" onClick={form.onReset}>
            Reset
          </button>

          <p>{form.error}</p>
        </form>
      </React.Fragment>
    );
  }
}

export default Login;
