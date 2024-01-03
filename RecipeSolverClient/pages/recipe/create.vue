<template>
  <div>
    <h1>Stwórz przepis</h1>
    <div>
      <v-form class="recipe-form" @submit.prevent="submitRecipe">
        <label>Tytuł: </label>
        <input id="title" v-model="recipe.title" type="text" placeholder="Wpisz tytuł przepisu" />
        <div class="input-row">
          <div class="input-col">
            <label>Czas przygotowania</label>
            <input id="preparationTime" v-model.number="recipe.preparationTime" type="number"
              placeholder="Wpisz czas w minutach" />
          </div>
          <div class="input-col">
            <label>Kategoria</label>
            <select v-model="recipe.category">
              <option value="śniadanie">Śniadanie</option>
              <option value="obiad">Obiad</option>
              <option value="kolacja">Kolacja</option>
              <option value="deser">Deser</option>
              <option value="przekąska">Przekąska</option>
            </select>
          </div>
          <div>
            <label>Wybierz składnik:</label>
            <select v-model="selectedIngredient">
              <option v-for="product in foodProducts" :key="product.id" :value="product.id">
                {{ product.name }}({{ product.unit }})
              </option>
            </select>
            <label>Ilość:</label>
            <input v-model.decimal="selectedQuantity" type="number" />

            <button @click="addIngredient">Dodaj składnik</button>


            <ul>
              Składniki:
              <li v-for="(ingredient, index) in recipe.ingredients" :key="index">
                {{ foodProducts.find(p => p.id === ingredient.productId).name }} - {{ ingredient.quantity }}
                <button class="remove-button" @click="removeIngredient(index)">X</button>
              </li>
            </ul>
          </div>
        </div>
        <button type="submit">Dodaj przepis</button>
        <ValidationErrorMessage :validationMessage="validationMessage" :showPopup="showPopup" />
      </v-form>
    </div>

  </div>
</template>
  
<script>
import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'

export default {
  auth: false,
  layout: 'default',
  components: {
    ValidationErrorMessage,
  },
  data() {
    return {
      recipe: {
        title: '',
        preparationTime: 0,
        category: '',
        coldDish: false,
        portions: 0,
        description: '',
        ingredients: [],
      },
      foodProducts: [],
      selectedIngredient: null,
      selectedQuantity: 0,
      validationMessage: '',
      showPopup: false,
    }
  },
  head() {
    return {
      title: 'Stwórz przepis',
    }
  },
  computed: {},
  methods: {
    async fetchFoodProducts() {
      try {
        const response = await this.$axios.get('/api/FoodProduct/GetAll')
        this.foodProducts = response.data
      } catch (error) {
      }
    },
    addIngredient() {
      if (this.selectedIngredient && this.selectedQuantity > 0) {
        const exists = this.recipe.ingredients.some(
          (ingredient) => ingredient.productId === this.selectedIngredient
        );

        if (!exists) {
          this.recipe.ingredients.push({
            productId: this.selectedIngredient,
            quantity: this.selectedQuantity,
          });
        }

        this.selectedIngredient = null;
        this.selectedQuantity = 0;
      }
    },
    removeIngredient(index) {
      this.recipe.ingredients.splice(index, 1);
    },
    async submitRecipe() {
      try {
        const response = await this.$axios.post(
          '/api/Recipe/Create',
          this.recipe
        )
      } catch (error) {
        this.validationMessage = error.message || 'Nie dodano przepisu'
        this.showPopup = true
        this.hideErrorMessageAfterDelay()      }
    },
    hideErrorMessageAfterDelay() {
      setTimeout(() => {
        this.validationMessage = ''
        this.showPopup = false
      }, 2000)
    },
  },
  async created() {
    await this.fetchFoodProducts()
  },
}
</script>

<style lang="scss" scoped>
@import '@/assets/recipe-create.scss';
</style>
  
  