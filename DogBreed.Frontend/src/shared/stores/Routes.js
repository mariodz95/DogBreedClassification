import { history } from "../../shared/utils/History";
import { RouterState } from "mobx-state-router";

export const routes = [
  {
    name: "dogbreed",
    pattern: "/dogbreed",
    beforeEnter: (fromState, toState, routerStore) => {
      const {
        rootStore: { dogBreedStore }
      } = routerStore;
      if (dogBreedStore.user === null) {
        return Promise.reject(new RouterState("login"));
      }
    }
  },
  {
    name: "login",
    pattern: "/login"
  },
  {
    name: "registration",
    pattern: "/"
  },
  {
    name: "results",
    pattern: "/results",
    onEnter: (fromState, toState, routerStore) => {
      const {
        rootStore: { dogBreedStore }
      } = routerStore;
      if (dogBreedStore.user === null) {
        return Promise.reject(new RouterState("login"));
      } else {
        dogBreedStore.changeLoading(true);
        dogBreedStore.getResults(true, 0);
        dogBreedStore.changeLoading(false);
      }
    }
  }
];
