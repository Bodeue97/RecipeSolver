<template>
  <div>
    <h1>Dodaj produkt do spiżarni</h1>
    <form class="pantry-form" @submit.prevent="submitForm">
      <label for="foodProductSelect">Wybierz produkt:</label>
      <select v-model="selectedFoodProductId" id="foodProductSelect">
        <option v-for="product in foodProducts" :key="product.id" :value="product.id">
          {{ product.name }}
        </option>
      </select>
      
      <label for="quantityInput">Ilość (g):</label>
      <input v-model.decimal="quantity" type="number" id="quantityInput">

      <button type="submit">Dodaj</button>
    </form>

    <ValidationErrorMessage :validationMessage="validationMessage" :showPopup="showPopup" />
  </div>
</template>

<script>
import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue'
export default {
  auth: true,
  layout: 'default',
  components: {
    ValidationErrorMessage,
  },
  data() {
    return {
      user: this.$auth.$storage.getUniversal('user'),
      validationMessage: '',
      showPopup: false,
      foodProducts: [],
      selectedFoodProductId: null,
      quantity: 0,


    }
  },
  head() {
    return {
      title: 'Spiżarnia',
    }
  },
  computed: {
    
  },
  methods: {
    async getFoodProducts() {
      try {

        const response = await this.$axios.get(
          `/api/FoodProduct/GetAll`
        )

        this.foodProducts = response.data

      } catch (error) {
        console.error('Błąd przy pobieraniu listy produktów:', error)

        this.showPopup = true
        this.validationMessage = error.message
        this.hideErrorMessageAfterDelay()
      }
    },
    async submitForm() {
      if (!this.selectedFoodProductId || this.quantity <= 0) {
        this.validationMessage = 'Wybierz produkt i podaj ilość';
        this.showPopup = true;
        this.hideErrorMessageAfterDelay();
        return; 
      }

      const data = {
        quantity: this.quantity,
        productId: this.selectedFoodProductId,
        userId: this.user.id
      };

      try {
        const response = await this.$axios.post('/api/PantryItem/Create', data);

        this.$router.push('/pantry/items')
      } catch (error) {
        console.error('Error creating pantry item:', error);
        this.validationMessage = 'Błąd przy tworzeniu pozycji w spiżarni';
        this.showPopup = true;
        this.hideErrorMessageAfterDelay();
      }
    }


  },
  async mounted(){
    this.getFoodProducts()
  }



}
</script>

<style lang="scss" scoped>
@import '@/assets/pantry.scss';
</style>