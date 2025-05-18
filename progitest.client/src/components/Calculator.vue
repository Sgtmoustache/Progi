<script setup>
import {onMounted, ref, watch} from 'vue';
import CalculatorAPI from '../services/api.js';

const carPrice = ref("");
const totalValue = ref({
  totalCost: null,
  sellerSpecialCost: null,
  buyerBaseCost: null,
  associationCost: null,
  storageCost: null
});
const calculatorAPI = new CalculatorAPI();
const selectedCarType = ref(null);

const carTypes = ref([]);

onMounted(async () => {
  try {
    carTypes.value = await calculatorAPI.getCarTypes();
    if (carTypes.value.length > 0) {
      selectedCarType.value = carTypes.value[0];
    }
  } catch (error) {
    console.error('Error fetching car types:', error);
  }
});

const refreshResult = async () => {

  if (carPrice.value !== "") {
    try {
      totalValue.value = await calculatorAPI.getCalculationResult(carPrice.value, selectedCarType.value);
    } catch (error) {
      console.error('Error fetching API value:', error);
    }
  } else {
    totalValue.value = {
      totalCost: null,
      sellerSpecialCost: null,
      buyerBaseCost: null,
      associationCost: null,
      storageCost: null
    };
  }
};

watch(carPrice, (newValue) => {
  refreshResult();
});

watch(selectedCarType, () => {
  refreshResult();
});

</script>

<template>
  <div class="calculator-container">
    <h1 class="calculator-title">Car Cost Calculator</h1>
    <div class="input-group">
      <label for="carPrice">Car Base Price: </label>
      <div class="input-with-unit">
        <input
          type="number"
          id="carPrice"
          v-model.number="carPrice"
          placeholder="Enter base price"
          class="input-field"
        />
        <span class="unit">$</span>
      </div>
    </div>
    <div class="input-group">
      <label>Type: </label>
      <select v-model="selectedCarType" class="select-field">
        <option v-for="type in carTypes" :key="type" :value="type">{{ type }}</option>
      </select>
    </div>
    <div class="cost-group">
      <div class="cost-item">
        <label>Base Cost: </label>
        <span>{{
            totalValue.buyerBaseCost !== null ? totalValue.buyerBaseCost.toFixed(2) : '-'
          }} $</span>
      </div>
      <div class="cost-item">
        <label>Special Cost: </label>
        <span>{{
            totalValue.sellerSpecialCost !== null ? totalValue.sellerSpecialCost.toFixed(2) : '-'
          }} $</span>
      </div>
      <div class="cost-item">
        <label>Association Cost: </label>
        <span>{{
            totalValue.associationCost !== null ? totalValue.associationCost.toFixed(2) : '-'
          }} $</span>
      </div>
      <div class="cost-item" style="border-bottom: 0">
        <label>Storage Cost: </label>
        <span>{{
            totalValue.storageCost !== null ? totalValue.storageCost.toFixed(2) : '-'
          }} $</span>
      </div>
    </div>
    <hr class="divider">
    <div class="total-group">
      <label>Total: </label>
      <span>{{ totalValue.totalCost !== null ? totalValue.totalCost.toFixed(2) : '-' }} $</span>
    </div>
  </div>
</template>

<style scoped>
.calculator-container {
  padding: 20px;
  border-radius: 8px;
  background-color: #f9f9f9;
}

.calculator-title {
  text-align: center;
  margin-bottom: 20px;
  color: #333;
  font-weight: bold;
}

.input-with-unit {
  position: relative;
}

.input-field {
  width: 100%;
  padding: 10px 30px 10px 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 16px;
  box-sizing: border-box;
}

.unit {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  color: #333;
  pointer-events: none;
}

.input-group {
  margin-bottom: 15px;
  color: #333;
}

.select-field {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 16px;
}

.cost-group {
  margin-top: 20px;
}

.cost-item {
  display: flex;
  justify-content: space-between;
  padding: 10px 0;
  border-bottom: 1px solid #e0e0e0;
}

.cost-item label {
  color: #555;
}

.cost-item span {
  color: #333;
}

.divider {
  margin: 20px 0;
  border: 1px solid #ccc;
}

.total-group {
  font-weight: bold;
  font-size: 18px;
  margin-top: 10px;
  color: #333;
}
</style>
