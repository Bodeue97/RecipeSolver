<template>
    <div class="recipe-grid">
        <div v-for="(recipe, index) in userRecipes" :key="index" class="recipe-tile">
            <img :src="`data:image/jpeg;base64,${recipe.photo}`" alt="Recipe Photo" />
            <p>{{ recipe.title }}</p>
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
        };
    },
    methods:{

        hideErrorMessageAfterDelay() {
      setTimeout(() => {
        this.validationMessage = ''
        this.showPopup = false
      }, 2000)
    },
    },
    async mounted() {
        try {
            const userId = this.user.id;
            const response = await this.$axios.get(`/api/Recipe/GetUsersRecipes/${userId}`);

            this.userRecipes = response.data;
          
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
@import '@/assets/recipe-list.scss';
</style>
  