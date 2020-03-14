import { RouterState, RouterStore } from "mobx-state-router";
import { routes } from "./Routes";
import { DogBreedStore } from "./DogBreedStore";
import { DogBreedAdapter } from "../../adapters/DogBreedAdapter";

const notFound = new RouterState("notFound");

export class RootStore {
  routerStore = new RouterStore(this, routes, notFound);
  dogBreedStore = new DogBreedStore(this);

  // Adapters for use by all stores
  adapters = {
    dogBreedAdapter: new DogBreedAdapter(this)
  };
}
