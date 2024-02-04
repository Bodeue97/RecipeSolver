<template>
    <div>
      <h1>Usuwanie produktów spożywczych</h1>
      <v-data-table class="centered-cells" :headers="headers" :items="foodProducts" :search="search">
        <template v-slot:[`item.actions`]="{ item }">
          <v-icon @click="deleteItem(item.foodProduct.id)">mdi-delete</v-icon>
        </template>
      </v-data-table>
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
        foodProducts: [], // Rename users to foodProducts
        headers: [
          { text: 'Działania', value: 'actions', align: 'center', sortable: false },
          { text: 'ID', value: 'foodProduct.id', align: 'right' }, // Update path
          { text: 'Nazwa', value: 'foodProduct.name', align: 'right' }, // Update path
        ],
        search: '',
      }
    },
    head() {
      return {
        title: 'Usuwanie produktów spożywczych',
      }
    },
    methods: {
      async getFoodProducts() { // Rename getUsers to getFoodProducts
        try {
          const response = await this.$axios.get(
            `/api/FoodProduct/GetAll`
          )
  
          this.foodProducts = response.data.map((item) => ({
            foodProduct: {
              id: item.id,
              name: item.name,
            },
          }))
        } catch (error) {
          console.error('Błąd przy pobieraniu produktów spożywczych:', error)
  
          this.showPopup = true
          this.validationMessage = error.message
          this.hideErrorMessageAfterDelay()
        }
      },
  
      async deleteItem(id) {
        try {
          await this.$axios.delete(`/api/FoodProduct/Delete/${id}`)
          this.getFoodProducts();
        } catch (error) {
          this.showPopup = true
          this.validationMessage = error.message
          this.hideErrorMessageAfterDelay()
        }
      },
  
      hideErrorMessageAfterDelay() {
        setTimeout(() => {
          this.validationMessage = ''
          this.showPopup = false
        }, 2000)
      },
    },
    async mounted() {
      this.getFoodProducts();
    },
  }
  </script>
  
  <style lang="scss" scoped>
  @import '@/assets/pantry.scss';
  
  .centered-cells .v-data-table-body td {
    width: 100px !important;
  }
  </style>
  