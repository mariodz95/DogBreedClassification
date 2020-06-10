import { action, runInAction, observable } from "mobx";
import LoginForm from "../../components/LoginForm";
import RegistrationForm from "../../components/RegistrationForm";

export class DogBreedStore {
  @observable dogBreedResults = [];
  @observable prediction = [];
  @observable uploadedImage = [];
  @observable displayButton = true;
  @observable isLoading = false;
  @observable noMoreResults = true;
  @observable showModal = false;
  userObject = null;
  data = 0;
  @observable passwordType = "password";
  @observable loginFailed = false;
  @observable registrationFailed = false;
  @observable toast = false;
  @observable user = null;

  constructor(rootStore) {
    this.rootStore = rootStore;
  }

  form = new LoginForm(this);
  regForm = new RegistrationForm(this);

  changePasswordType = () => {
    runInAction(() => {
      this.passwordType === "password"
        ? (this.passwordType = "text")
        : (this.passwordType = "password");
    });
  };

  @action displayToast = () => {
    runInAction(() => {
      this.toast = false;
    });
  };

  @action handleOpenModal = () => {
    runInAction(() => {
      this.showModal = true;
    });
  };

  @action handleCloseModal = () => {
    runInAction(() => {
      this.showModal = false;
    });
  };

  @action changeLoading() {
    runInAction(() => {
      this.isLoading = this.isLoading === false ? true : false;
    });
  }

  @action imageRemove() {
    runInAction(() => {
      this.uploadedImage = [];
    });
  }

  @action userRemove() {
    localStorage.clear();
    runInAction(() => {
      this.user = null;
    });
  }

  @action removeResult() {
    runInAction(() => {
      this.prediction = null;
    });
  }

  @action onDrop = (event) => {
    runInAction(() => {
      if (event.length > 0) {
        for (var i = 0; i < event.length; i++) {
          this.uploadedImage.push(event[i]);
          this.displayButton = false;
        }
      } else {
        this.uploadedImage = [];
        this.prediction = null;
        this.displayButton = true;
      }
    });
  };

  @action async getResults(refresh, rows) {
    if (refresh) {
      this.data = 0;
      runInAction(() => {
        this.dogBreedResults = [];
      });
    }
    const user = JSON.parse(localStorage.getItem("user"));

    this.isLoading = true;
    const result = await this.rootStore.adapters.dogBreedAdapter.getResults(
      rows,
      user.data.id
    );
    runInAction(() => {
      if (result) {
        if (refresh) {
          this.dogBreedResults = result;
        } else {
          let dogs = result.filter((e) => e !== "");
          this.dogBreedResults.push(...dogs);
        }
        const more = this.dogBreedResults.indexOf("") > -1;
        if (this.dogBreedResults.length % 5 === 0 && more === false) {
          this.noMoreResults = false;
        } else {
          this.noMoreResults = true;
        }

        this.isLoading = false;
      }
    });
  }

  @action async getPrediction() {
    this.isLoading = true;
    const user = JSON.parse(localStorage.getItem("user"));
    const result = [];
    for (let i = 0; i < this.uploadedImage.length; i++) {
      const formData = new FormData();
      formData.append(
        "formData",
        this.uploadedImage[i],
        this.uploadedImage[i].name
      );
      result.push(
        await this.rootStore.adapters.dogBreedAdapter.getPrediction(
          formData,
          user.data.id
        )
      );
    }

    runInAction(() => {
      this.prediction = result;
      this.isLoading = false;
    });
  }

  @action async registration(email, password) {
    const result = await this.rootStore.adapters.dogBreedAdapter.registration(
      email,
      password
    );
    if (result.data !== "") {
      this.rootStore.routerStore.goTo("dogbreed");
      runInAction(() => {
        this.toast = true;
      });
    } else {
      runInAction(() => {
        this.registrationFailed = true;
      });
    }
  }

  @action async login(email, password) {
    const result = await this.rootStore.adapters.dogBreedAdapter.login(
      email,
      password
    );
    if (result.data.length === undefined) {
      runInAction(() => {
        this.user = result;
        // setter
        localStorage.setItem("user", JSON.stringify(result));
        this.toast = true;
      });
      this.rootStore.routerStore.goTo("dogbreed");
    } else {
      runInAction(() => {
        this.loginFailed = true;
      });
    }
  }
}
