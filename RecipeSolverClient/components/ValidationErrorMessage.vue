<template>
    <transition name="fade">
      <div v-if="showPopup" class="validation-popup">
        <p>{{ formattedValidationMessage }}</p>
      </div>
    </transition>
  </template>
  
  <script>
  export default {
    props: {
      validationMessage: {
        type: String,
        required: true,
      },
      showPopup: {
        type: Boolean,
        default: false,
      },
      cautionIcon: {
        type: String,
        default: "⚠️",
      },
    },
    computed: {
      formattedValidationMessage() {
        return `${this.cautionIcon} ${this.validationMessage}`;
      },
    },
  };
  </script>
  
  <style scoped>
  .validation-popup {
    position: fixed;
    top: 20px;
    right: 40px; /* Adjust right position for top-right corner */
    background-color: orange;
    color: white;
    padding: 15px 25px; /* Adjust padding for size */
    border-radius: 5px;
    z-index: 999; /* Ensure it appears above other content */
    font-size: 20px; /* Increase font size */
    transform: scale(1.2); /* Make it 20% larger */
  }
  
  .fade-enter-active {
    transition: opacity 0.5s; /* Adjust transition duration for entering */
  }
  
  .fade-enter {
    opacity: 0;
  }
  
  .fade-leave-active {
    transition: opacity 0.5s; /* Adjust fade-out duration here */
  }
  
  .fade-leave-to {
    opacity: 0;
  }
  </style>
  