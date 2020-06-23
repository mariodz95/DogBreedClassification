import { RouterState } from "mobx-state-router";

export const routes = [
  {
    name: "dogbreed",
    pattern: "/dogbreed",
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
      dogBreedStore.changeLoading();
      dogBreedStore.changeButton();
      dogBreedStore.getResults(true, 0);
    },
  },
];
