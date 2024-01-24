<template>
    <div class="recipe-container" v-if="recipe">
        <div class="recipe-photo">
            <img :src="`data:image/jpeg;base64,${recipe.photo}`" alt="Recipe Photo" class="photo" />
        </div>
        <div class="recipe-info">
            <h1 class="recipe-title silver-text">{{ recipe.title }}</h1>
            <h2 class="recipe-title silver-text"> Czas przygotowania: {{ recipe.preparationTime }} min.</h2>
            <h3 class="recipe-title silver-text"> Kategoria: {{ recipe.category }}</h3>
            <h4 class="recipe-title silver-text">Danie na zimno?: {{ recipe.coldDish ? 'Tak' : 'Nie' }}</h4>
            <div class="rating-container">
                <span v-for="star in 5" :key="star" :class="{ 'star-filled': star <= userRating }"
                    @click="rateRecipe(star)">
                    ★
                </span>
            </div>



        </div>


        <v-row>
            <v-col cols="4">
                <div class="ingredients-container">
                    <h3>Lista składników:</h3>
                    <ul>
                        <li v-for="(ingredient, index) in recipe.ingredients" :key="index">
                            {{ ingredient.product.name }} - {{ ingredient.quantity }} {{ ingredient.product.unit }}
                        </li>
                    </ul>
                </div>
            </v-col>

            <v-col cols="8">
                <div class="description-container">
                    <h3>Opis:</h3>
                    <p>{{ recipe.description }}</p>
                </div>
            </v-col>

            <v-col cols="4">
                <div class="nutritional-values-container">
                    <h3>Wartości odżywcze:</h3>
                    <ul>
                        <li v-for="(value, key) in recipe.totalNutrition" :key="key"
                            v-if="key !== 'id' && key !== 'recipeId'">
                            {{ translateKey(key).name }}: {{ value }} {{ translateKey(key).unit }}
                        </li>
                    </ul>
                </div>
            </v-col>
        </v-row>
    </div>

    <div class="loader-container" v-else>
        <v-progress-circular class="loader" indeterminate color="primary"></v-progress-circular>
        <ValidationErrorMessage v-if="validationMessage" :message="validationMessage" />
    </div>
</template>
  
<script>
import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'

export default {
    components: {
        ValidationErrorMessage,
    },
    auth: false,
    layout: 'default',
    data() {
        return {
            user: this.$auth.$storage.getUniversal('user'),
            userRating: 0,
            recipe: null,
            validationMessage: "",
            showPopup: false,
        };
    },
    computed: {
        isColdDish() {
            return this.recipe ? (this.recipe.coldDish ? 'Tak' : 'Nie') : '';
        }
    },
    async mounted() {
        const recipeId = this.$route.params.id;
        try {
            const response = await this.$axios.get(`/api/Recipe/Get/${recipeId}`);
            this.recipe = response.data;
            this.userRating = Math.round(this.recipe.overallRating);

        } catch (error) {
            this.validationMessage = error.message;
            this.showPopup = true;
            this.hideErrorMessageAfterDelay();
        }


    },
    head() {
        return {
            title: 'Szczegóły przepisu'
        };
    },
    methods: {
        async rateRecipe(rating) {
            this.userRating = rating;

            const roundedRating = Math.round(rating * 2) / 2;

            for (let i = 1; i <= 5; i++) {
                const starElement = document.getElementById(`star${i}`);
                if (starElement) {
                    const starValue = i / 2; 
                    starElement.classList.toggle('star-filled', starValue <= roundedRating);
                }
            }

            try {
                const response = await this.$axios.patch(`/api/Recipe/RateRecipe/${this.recipe.id}`, {
                    userId: this.user.id,
                    rating: roundedRating,
                });
                this.recipe.overallRating = response.data.updatedRating;
            } catch (error) {
                this.validationMessage = error.message;
                this.showPopup = true;
                this.hideErrorMessageAfterDelay();
            }
        },
        translateKey(key) {
            const translations = {
                energyValue: { name: 'Wartość energetyczna', unit: 'kcal' },
                protein: { name: 'Białko', unit: 'g' },
                totalFat: { name: 'Tłuszcz', unit: 'g' },
                saturatedFat: { name: 'Tłuszcze nasycone', unit: 'g' },
                monounsaturatedFat: { name: 'Tłuszcze jednonienasycone', unit: 'g' },
                polyunsaturatedFat: { name: 'Tłuszcze wielonienasycone', unit: 'g' },
                cholesterol: { name: 'Cholesterol', unit: 'mg' },
                digestibleCarbohydrates: { name: 'Węglowodany przyswajalne', unit: 'g' },
                glucose: { name: 'Glukoza', unit: 'g' },
                fructose: { name: 'Fruktoza', unit: 'g' },
                sucrose: { name: 'Sacharoza', unit: 'g' },
                lactose: { name: 'Laktoza', unit: 'g' },
                fiber: { name: 'Błonnik', unit: 'g' },
                sodium: { name: 'Sód', unit: 'mg' },
                potassium: { name: 'Potas', unit: 'mg' },
                calcium: { name: 'Wapń', unit: 'mg' },
                phosphorus: { name: 'Fosfor', unit: 'mg' },
                magnesium: { name: 'Magnez', unit: 'mg' },
                iron: { name: 'Żelazo', unit: 'mg' },
                vitaminA: { name: 'Witamina A', unit: 'µg' },
                betaCarotene: { name: 'Beta-karoten', unit: 'µg' },
                vitaminD: { name: 'Witamina D', unit: 'µg' },
                vitaminE: { name: 'Witamina E', unit: 'mg' },
                thiamine: { name: 'Tiamina (Witamina B1)', unit: 'mg' },
                riboflavin: { name: 'Ryboflawina (Witamina B2)', unit: 'mg' },
                niacin: { name: 'Niacyna (Witamina B3)', unit: 'mg' },
                vitaminC: { name: 'Witamina C', unit: 'mg' },
            };
            return translations[key] || key;
        },

    },
};
</script>
<style lang="scss" scoped>
@import"@/assets/recipe-details.scss"
</style>