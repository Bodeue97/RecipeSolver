<template>
  <div>
    <h1>Stwórz przepis</h1>
    <div class="recipe-form">
      <v-form @submit.prevent="submitRecipe">
        <label>Tytuł</label>
        <input
          id="title"
          v-model="recipe.title"
          type="text"
          placeholder="Wpisz tytuł przepisu"
        />
        <div class="input-row">
          <div class="input-col">
            <label>Czas przygotowania</label>
            <input
              id="preparationTime"
              v-model.number="recipe.preparationTime"
              type="number"
              placeholder="Wpisz czas w minutach"
            />
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
        </div>
        <button type="submit">Dodaj przepis</button>
      </v-form>
    </div>
  </div>
</template>
  
  <script>
export default {
  auth: false,
  layout: 'default',
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
        console.log(this.foodProducts)
      } catch (error) {
        console.error('Error fetching food products:', error)
      }
    },
    addIngredient() {
      if (this.selectedIngredient && this.selectedQuantity) {
        this.recipe.ingredients.push({
          productId: this.selectedIngredient,
          quantity: this.selectedQuantity,
        })
        this.selectedIngredient = null
        this.selectedQuantity = 0
      }
    },
    async submitRecipe() {
      try {
        const response = await this.$axios.post(
          '/api/Recipe/Create',
          this.recipe
        )
        console.log('Recipe created:', response.data)
      } catch (error) {
        console.error('Error creating recipe:', error)
      }
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
  
  