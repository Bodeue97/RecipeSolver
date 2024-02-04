<template>
    <div>
        <h1>Zmień ilość produktu</h1>
        <form class="pantry-edit" @submit.prevent="updatePantryItem">
            <div v-if="pantryItem">
                <label for="pantryItemName">Produkt:</label>
                <span class="product-span" id="pantryItemName">{{ pantryItem.product.name }}</span>

                <label for="quantityInput">Nowa ilość (g):</label>
                <input v-model.decimal="newQuantity" type="number" step="0.01" id="quantity">
            </div>

            <button type="submit">Aktualizuj ilość</button>
        </form>

        <ValidationErrorMessage :validationMessage="validationMessage" :showPopup="showPopup" />
    </div>
</template>
  
<script>
import ValidationErrorMessage from '~/components/ValidationErrorMessage.vue';

export default {
    auth: true,
    layout: 'default',
    components: {
        ValidationErrorMessage,
    },
    data() {
        return {

            pantryItem: null,
            newQuantity: null,
            validationMessage: '',
            showPopup: false,
        };
    },
    head() {
        return {
            title: 'Spiżarnia',
        };
    },
    async mounted() {
        const id = this.$route.params.id;

        try {
            const response = await this.$axios.get(`/api/PantryItem/Get/${id}`);

            this.pantryItem = response.data[0];
            this.newQuantity = this.pantryItem.quantity;
        } catch (error) {
            this.validationMessage = 'Error fetching pantry item';
            this.showPopup = true;
            this.hideErrorMessageAfterDelay();
        }
    },
    methods: {
        async updatePantryItem() {
            if (!this.pantryItem || this.newQuantity <= 0) {
                this.validationMessage = 'Invalid quantity';
                this.showPopup = true;
                this.hideErrorMessageAfterDelay();
                return;
            }

            try {


                const data = {
                    quantity: this.newQuantity
                };

                await this.$axios.patch(`/api/PantryItem/Update/${this.pantryItem.id}`, data);


                this.$router.push('/pantry/items');
            } catch (error) {
                this.validationMessage = error.message;
                this.showPopup = true;
                this.hideErrorMessageAfterDelay();
            }
        },
        hideErrorMessageAfterDelay() {
            setTimeout(() => {
                this.validationMessage = '';
                this.showPopup = false;
            }, 2000);
        },
    },
};
</script>
  
<style lang="scss" scoped>
@import '@/assets/pantry.scss';
</style>
  