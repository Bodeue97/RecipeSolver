<template>
    <v-app>
        <v-form @submit.prevent="applyFilters">
            <v-text-field v-model="filters.title" label="Tytuł"></v-text-field>

            <v-text-field v-model="filters.preparationTime" label="Maksymalny czas przygotowania"
                type="number"></v-text-field>

            <v-select v-model="filters.category" :items="categoryOptions" label="Kategoria"></v-select>

            <v-checkbox v-model="filters.coldDish" label="Danie na zimno?"></v-checkbox>

            <v-row>
                <v-col v-for="(value, key) in filters.nutritionalValues" :key="key" cols="12" md="6">
                    <v-text-field v-model="filters.nutritionalValues[key][0]"
                        :label="`${nutritionalValues[key].name} (${nutritionalValues[key].unit}) (od)`"
                        type="number"></v-text-field>
                    <v-text-field v-model="filters.nutritionalValues[key][1]"
                        :label="`${nutritionalValues[key].name} (${nutritionalValues[key].unit}) (do)`"
                        type="number"></v-text-field>
                </v-col>
            </v-row>

            <v-btn type="submit" color="primary">Apply Filters</v-btn>
        </v-form>
    </v-app>
</template>
  
<script>
export default {
    data() {
        return {
            filters: {
    title: null,
    preparationTime: null,
    category: null,
    coldDish: false,
    nutritionalValues: {
        energyValue: [null, null],
        protein: [null, null],
        totalFat: [null, null],
        saturatedFat: [null, null],
        monounsaturatedFat: [null, null],
        polyunsaturatedFat: [null, null],
        cholesterol: [null, null],
        digestibleCarbohydrates: [null, null],
        glucose: [null, null],
        fructose: [null, null],
        sucrose: [null, null],
        lactose: [null, null],
        fiber: [null, null],
        sodium: [null, null],
        potassium: [null, null],
        calcium: [null, null],
        phosphorus: [null, null],
        magnesium: [null, null],
        iron: [null, null],
        vitaminA: [null, null],
        betaCarotene: [null, null],
        vitaminD: [null, null],
        vitaminE: [null, null],
        thiamine: [null, null],
        riboflavin: [null, null],
        niacin: [null, null],
        vitaminC: [null, null]
    }
},

            categoryOptions: ['Śniadanie', 'Obiad', 'Kolacja', 'Deser', 'Przekąska'],
            nutritionalValues: {
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
            }
        };


    },


    methods: {
        applyFilters() {
            this.$emit('filters-applied', this.filters);
        },
    },
};
</script>
  
<style lang="scss" scoped></style>
  