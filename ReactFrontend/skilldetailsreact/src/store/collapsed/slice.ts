import { createSlice } from "@reduxjs/toolkit";


const collapsedSlice = createSlice({
  name: "collapsed",
  initialState: true,
  reducers: {
    isCollapsed: (state) => {
      return state = !state
    },
  },
});

export const { reducer, actions } = collapsedSlice;
export const { isCollapsed } = actions;
