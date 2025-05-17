<script setup>
import { ref, onMounted } from 'vue';
import CalculatorAPI from '../services/api.js';

const basePrice = ref(0);
const totalValue = ref(null);
const calculatorAPI = new CalculatorAPI();
const selectedCarType = ref(null);

const carTypes = ref([]);

onMounted(async () => {
  try {
    carTypes.value = await calculatorAPI.getCarTypes();
  } catch (error) {
    console.error('Error fetching car types:', error);
  }
});

// Function to fetch the API value
const getTotal = async () => {
  try {
    totalValue.value = await calculatorAPI.getTotalValue(basePrice.value, selectedCarType.value);
  } catch (error) {
    console.error('Error fetching API value:', error);
    totalValue.value = null; // Reset the value on error
  }
};
</script>

<template>
  <div>
    <h1>Calculator</h1>
    <div>
      <label for="basePrice">Prix de base du véhicule:</label>
      <input
        type="number"
        id="basePrice"
        v-model.number="basePrice"
      placeholder="Entrez le prix de base"
      />
    </div>
    <p>Base Price: {{ basePrice }}</p>
    <select v-model="selectedCarType">
      <option v-for="type in carTypes" :key="type" :value="type">{{ type }}</option>
    </select>
    <div v-if="totalValue !== null">
      <label>API Value:</label>
      <span>{{ totalValue }}</span>
    </div>
    <button @click="getTotal">Get total cost</button>
  </div>
</template>
