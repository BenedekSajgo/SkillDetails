import { combineReducers, configureStore } from "@reduxjs/toolkit";
import {reducer as collapsedReducer} from "./collapsed/slice"
import { loadState, saveState } from "./localStorage";
import debounce from "debounce"

const rootReducer = combineReducers({
    collapsed: collapsedReducer
})

export const store = configureStore({
    reducer: rootReducer,
    preloadedState: loadState(),
})

store.subscribe(
    debounce(() => {
        saveState(store.getState())
    }, 5000)
)

