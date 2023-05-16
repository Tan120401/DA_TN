<template>
    <div class="loading">
        <img src="@/assets/img/Loading.gif" alt="Loading" />
      </div>
  </template>
  
  <script>
  export default {
      name: "TLoading"
  }
  </script>
  
  <style>
  .loading {
      display: flex;
      position: absolute;
      top: 50%;
      left: 55%;
      transform: translate(-50%,-50%);
      justify-content: center;
      align-items: center;
      width: 40px;
      z-index: 1000;
    }
  </style>