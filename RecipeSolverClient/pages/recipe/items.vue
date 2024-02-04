<template>
    <div>
        <v-btn @click="toggleFilters" class="toggle-filters-button">Filtruj</v-btn>
        <Filters v-if="filtersVisible" />
        <div class="recipe-grid">
            <div v-if="isLoading" class="loader">
                <v-progress-circular indeterminate color="primary"></v-progress-circular>
            </div>
            <div v-for="(recipe, index) in recipes" :key="index" class="recipe-tile">
                <a :href="`/recipe/${recipe.id}`" class="recipe-link">
                    <div class="recipe-image-container">
                        <span v-if="recipe.overallRating" class="rating-label">Ocena: {{ recipe.overallRating }}</span>
                        <span class="ratings-number">Ilość ocen: {{ recipe.ratingsNumber }}</span>
                        <span class="label-over-photo">{{ recipe.title }}</span>
                        <span class="delete-over-photo"><v-btn><v-icon>mdi-delete</v-icon></v-btn></span>
                        <img :src="`data:image/jpeg;base64,${recipe.photo}`" alt="Recipe Photo" class="recipe-image" />
                    </div>
                </a>
            </div>
            <ValidationErrorMessage :validationMessage="validationMessage" :showPopup="showPopup" />
        </div>
    </div>
</template>
  
<script>
import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'
import Filters from '~/components/Filters.vue'

export default {
    components: {
        ValidationErrorMessage,
        Filters
    },
    auth: false,
    layout: 'default',
    data() {
        return {
            recipes: [],
            validationMessage: "",
            showPopup: false,
            isLoading: true,
            filtersVisible: false,

        };
    },
    methods: {
        hideErrorMessageAfterDelay() {
            setTimeout(() => {
                this.validationMessage = ''
                this.showPopup = false
            }, 2000)
        },
        toggleFilters() {
            this.filtersVisible = !this.filtersVisible;
        },
    },
    async mounted() {
        try {
            this.isLoading = true
            const response = await this.$axios.get(`/api/Recipe/GetAll`);

            this.recipes = response.data;
            this.isLoading = false;

        } catch (error) {
            this.validationMessage = error.message;
            this.showPopup = true;
            this.hideErrorMessageAfterDelay();
        }
    },
    head() {
        return {
            title: 'Przeglądaj przepisy'
        };
    }
};
</script>
  
  
<style lang="scss" scoped>
@import"@/assets/recipe-list.scss"
</style>
  