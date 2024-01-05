<template>
    <div class="recipe-grid">
      <div v-if="isLoading" class="loader">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
      </div>
      <div v-for="(recipe, index) in userRecipes" :key="index" class="recipe-tile">
        <a :href="`/recipe/${recipe.id}`" class="recipe-link">
          <div class="recipe-image-container">
            <span v-if="recipe.overallRating" class="rating-label">{{ recipe.overallRating }}</span>
            <span class="ratings-number">Ilość ocen: {{ recipe.ratingsNumber }}</span>
            <span class="label-over-photo">{{ recipe.title }}</span>
            <img :src="`data:image/jpeg;base64,${recipe.photo}`" alt="Recipe Photo" class="recipe-image" />
          </div>
        </a>
      </div>
      <ValidationErrorMessage :validationMessage="validationMessage" :showPopup="showPopup" />
    </div>
  </template>
  
  <script>
  import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'
  
  export default {
    components: {
      ValidationErrorMessage,
    },
    auth: true,
    layout: 'default',
    data() {
      return {
        user: this.$auth.$storage.getUniversal('user'),
        userRecipes: [],
        validationMessage: "",
        showPopup: false,
        isLoading: true,
      };
    },
    methods: {
      hideErrorMessageAfterDelay() {
        setTimeout(() => {
          this.validationMessage = ''
          this.showPopup = false
        }, 2000)
      },
    },
    async mounted() {
      try {
        this.isLoading = true
        const userId = this.user.id;
        const response = await this.$axios.get(`/api/Recipe/GetUsersRecipes/${userId}`);
  
        this.userRecipes = response.data;
        this.isLoading = false;
  
      } catch (error) {
        this.validationMessage = error.message;
        this.showPopup = true;
        this.hideErrorMessageAfterDelay();
      }
    },
    head() {
      return {
        title: 'Moje przepisy'
      };
    }
  };
  </script>
  
  
<style lang="scss" scoped>
@import"@/assets/recipe-list.scss"
</style>
  