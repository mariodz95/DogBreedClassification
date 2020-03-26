import { action, runInAction, observable } from "mobx";
import RegistrationForm from "../../components/RegistrationForm";
import Registration from "../../components/Registration";

export class DogBreedStore {
  @observable dogBreedResults = [];
  @observable prediction = [];
  @observable uploadedImage = null;
  @observable isLoading = false;
  @observable noMoreResults = false;
  @observable showModal = false;
  userObject = null;
  data = 0;

  constructor(rootStore) {
    this.rootStore = rootStore;
  }

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
    this.isLoading = false;
  }

  @action imageRemove() {
    runInAction(() => {
      this.uploadedImage = null;
    });
  }

  @action removeResult() {
    runInAction(() => {
      this.prediction.data = null;
    });
  }

  @action onDrop = event => {
    runInAction(() => {
      if (event.length > 0) {
        this.uploadedImage = event[0];
      } else {
        this.uploadedImage = null;
        this.prediction.data = null;
      }
    });
  };

  @action async getResults(refresh, rows) {
    if (refresh) {
      this.dogBreedResults = [];
      this.data = 0;
      runInAction(() => {
        this.noMoreResults = false;
      });
    }

    this.isLoading = true;
    const result = await this.rootStore.adapters.dogBreedAdapter.getResults(
      rows
    );
    runInAction(() => {
      if (result) {
        if (refresh) {
          this.dogBreedResults = result;
        } else {
          this.dogBreedResults.push(...result);
          console.log("sasda", this.dogBreedResults.length);
        }
        if (this.dogBreedResults.length % 5 > 0) {
          this.noMoreResults = true;
        }
        this.isLoading = false;
      }
    });
  }

  @action async getPrediction(formData) {
    this.isLoading = true;
    const result = await this.rootStore.adapters.dogBreedAdapter.getPrediction(
      formData
    );
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
  }

  @action async login(email, password) {
    const result = await this.rootStore.adapters.dogBreedAdapter.login(
      email,
      password
    );
    return await result;
  }
}
