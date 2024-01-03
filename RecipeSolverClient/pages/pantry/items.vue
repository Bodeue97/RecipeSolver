<template>
  <div>
    <h1>Spiżarnia</h1>
    <v-btn class="pantry-create-button" @click="goToCreatePantry">Dodaj produkt do spiżarni</v-btn>
    <v-data-table class="centered-cells" :headers="headers" :items="pantryItems" :search="search">
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon @click="editItem(item.id)">mdi-pencil</v-icon>
        <v-icon @click="deleteItem(item.id)">mdi-delete</v-icon>
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
      pantryItems: [],
      headers: [
        { text: 'Działania', value: 'actions', align: 'center', sortable: false },
        { text: 'Nazwa produktu', value: 'product.name', align: 'right' },
        { text: 'Kategoria', value: 'product.type', align: 'center' },
        { text: 'Ilość ', value: 'quantity', align: 'center' },
        { text: 'Jednostka', value: 'product.unit', align: 'center', sortable: false }

      ],
      search: '',
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
    async getPantryItems() {
      try {
        const id = this.user.id
        const token = this.user.token
        const response = await this.$axios.get(
          `/api/PantryItem/GetUsersItems/${id}/${token}`
        )

        this.pantryItems = response.data.map((item) => ({
          quantity: item.quantity,
          product: {
            id: item.id,
            name: item.product.name,
            type: item.product.type,
            unit: item.product.unit,
          },
          id: item.id,
        }))
      } catch (error) {
        console.error('Błąd przy pobieraniu produktów ze spiżarni:', error)

        this.showPopup = true
        this.validationMessage = error.message
        this.hideErrorMessageAfterDelay()
      }
    },
    goToCreatePantry() {
      this.$router.push('/pantry/create')
    },
    async editItem(id) {

      this.$router.push("/pantry/edit/" + id)
    },

    async deleteItem(id) {
      try {
        const request = await this.$axios.delete(`/api/PantryItem/Delete/${id}`)
        this.getPantryItems();
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
    this.getPantryItems();
  },
}
</script>

<style lang="scss" scoped>
@import '@/assets/pantry.scss';

.centered-cells .v-data-table-body td {
  width: 100px !important;
}
</style>