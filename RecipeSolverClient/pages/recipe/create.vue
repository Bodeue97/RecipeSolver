<template>
  <div>
    <h1>Stwórz przepis</h1>
    <div>
      <v-form class="recipe-form" @submit.prevent="submitRecipe">
        <label>Tytuł: </label>
        <input id="title" v-model="recipe.title" type="text" placeholder="Wpisz tytuł przepisu" />
        <div class="input-row">
          <div class="input-col">
            <label>Czas przygotowania w minutach: </label>
            <input id="preparationTime" v-model.number="recipe.preparationTime" type="number"
              placeholder="Wpisz czas w minutach" />
          </div>
          <div class="input-col">
            <label>Kategoria: </label>
            <select v-model="recipe.category">
              <option value="Śniadanie">Śniadanie</option>
              <option value="Obiad">Obiad</option>
              <option value="Kolacja">Kolacja</option>
              <option value="Deser">Deser</option>
              <option value="Przekąska">Przekąska</option>
            </select>
          </div>

          <div>
            <label for="coldDishCheckbox">Danie zimne?</label>
            <input id="coldDishCheckbox" v-model="recipe.coldDish" type="checkbox" />
          </div>

          <div>
            <label for="portionsInput">Ilość porcji:</label>
            <input id="portionsInput" v-model.number="recipe.portions" type="number" />
          </div>

          <div>
            <label for="descriptionTextarea">Opis:</label>
            <textarea id="descriptionTextarea" v-model="recipe.description" placeholder="Wpisz opis przepisu"></textarea>
          </div>

          <div>
            <label>Wybierz składnik:</label>
            <select v-model="selectedIngredient">
              <option v-for="product in foodProducts" :key="product.id" :value="product">
                {{ product.name }} ({{ product.unit }})
              </option>
            </select>
            <label>Ilość:</label>
            <input v-model.decimal="selectedQuantity" type="number" />

            <button type="button" @click="addIngredient">Dodaj składnik</button>


            <ul>
              Składniki:
              <li v-for="(ingredient, index) in recipe.ingredients" :key="index">
                {{ foodProducts.find(p => p.id === ingredient.productId).name }} - {{ ingredient.quantity }} ({{
                  ingredient.unit }})
                <button class="remove-button" @click="removeIngredient(index)">X</button>
              </li>
            </ul>
          </div>

          <div class="photo-input">
            <input type="file" @change="onFileChange" accept="image/*" />
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
      user: this.$auth.$storage.getUniversal('user'),

      recipe: {
        title: '',
        preparationTime: 0,
        category: '',
        coldDish: false,
        portions: 0,
        description: '',
        ingredients: [],
        photo: null,
        userId: null,
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
          (ingredient) => ingredient.productId === this.selectedIngredient.id
        );

        if (!exists) {
          this.recipe.ingredients.push({
            productId: this.selectedIngredient.id,
            quantity: this.selectedQuantity,
            unit: this.selectedIngredient.unit,
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
      if (
        !this.recipe.title ||
        this.recipe.preparationTime <= 0 ||
        !this.recipe.category ||
        !this.recipe.description ||
        this.recipe.ingredients.length === 0 ||
        !this.recipe.photo
      ) {
        this.validationMessage = 'Wypełnij wszystkie pola przed dodaniem przepisu.';
        this.showPopup = true;
        this.hideErrorMessageAfterDelay();
        return;
      }
      try {
        const formattedIngredients = this.recipe.ingredients.map(({ productId, quantity }) => ({ productId, quantity }));

        this.recipe.ingredients = formattedIngredients;
        this.recipe.userId = this.user.id;
        const response = await this.$axios.post(
          '/api/Recipe/Create',
          this.recipe
        );

        this.recipe = {
          title: '',
          preparationTime: 0,
          category: '',
          coldDish: false,
          portions: 0,
          description: '',
          ingredients: [],
          photo: null,
        };
        this.validationMessage = 'Przepis został dodany.';
        this.showPopup = true;
        this.hideErrorMessageAfterDelay();
        this.$router.push("/recipe/items")

      } catch (error) {
        this.validationMessage = error.message || 'Nie dodano przepisu';
        this.showPopup = true;
        this.hideErrorMessageAfterDelay();
      }
    },

    onFileChange(event) {
      const selectedFile = event.target.files[0];
      this.readFile(selectedFile);
    },

    async readFile(file) {
      const reader = new FileReader();

      reader.onload = () => {
        const base64data = reader.result.split(',')[1]; 
        this.recipe.photo = base64data; 
      };

      reader.readAsDataURL(file); 
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
  
  