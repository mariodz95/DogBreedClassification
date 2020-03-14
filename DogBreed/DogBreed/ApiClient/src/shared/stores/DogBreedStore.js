import { action, runInAction, observable } from "mobx";

export class DogBreedStore {
  @observable dogBreedResults = [];
  @observable prediction = [];
  @observable uploadedImage = null;
  @observable isLoading = false;

  constructor(rootStore) {
    this.rootStore = rootStore;
  }

  @action imageRemove() {
    runInAction(() => {
      this.uploadedImage = null;
      console.log(this.uploadedImage);
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

  @action async getResults() {
    const result = await this.rootStore.adapters.dogBreedAdapter.getResults();
    runInAction(() => {
      this.dogBreedResults = result;
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
}
