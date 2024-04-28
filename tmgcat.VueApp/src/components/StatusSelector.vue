<template>
    <button class="btn">
      <slot></slot>
    </button>
  </template>
  
  <script>
  export default {
    name: 'status-selector'
  }
  </script>
  
  <style>
  .btn {
    padding: 10px 15px;
    background: none;
    color: rgb(150, 150, 150);
    border: 1px solid rgb(57, 57, 57);
  }
  </style>