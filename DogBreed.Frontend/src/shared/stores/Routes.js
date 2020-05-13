import { RouterState } from "mobx-state-router";

export const routes = [
  {
    name: "dogbreed",
    pattern: "/dogbreed",
    beforeEnter: (fromState, toState, routerStore) => {
      const {
        rootStore: { dogBreedStore },
      } = routerStore;
      const user = localStorage.getItem("user");
      if (user === null) {
        return Promise.reject(new RouterState("login"));
      }
    },
  },
  {
    name: "login",
    pattern: "/login",
  },
  {
    name: "registration",
    pattern: "/",
  },
  {
    name: "results",
    pattern: "/results",
    onEnter: (fromState, toState, routerStore) => {
      const {
        rootStore: { dogBreedStore },
      } = routerStore;
      const user = localStorage.getItem("user");
      if (user === null) {
        return Promise.reject(new RouterState("login"));
      } else {
        dogBreedStore.changeLoading();
        dogBreedStore.getResults(true, 0);
      }
    },
  },
];
