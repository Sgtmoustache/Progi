class CalculatorAPI {
  constructor() {
    this.baseURL = 'https://localhost:7210';
  }

  async getTotalValue(basePrice, type) {
    try {
      const response = await fetch(`${this.baseURL}/Calculator?basePrice=${basePrice}&type=${type}`);
      if (!response.ok) {
        throw new Error('Network response was not ok ' + response.statusText);
      }
      const totalValue = await response.json();
      return totalValue;
    } catch (error) {
      console.error('Error fetching total value:', error);
      throw error;
    }
  }

  async getCarTypes() {
    try {
      const response = await fetch(`${this.baseURL}/Calculator/car-types`);
      if (!response.ok) {
        throw new Error('Network response was not ok ' + response.statusText);
      }
      const carTypes = await response.json();
      return carTypes;
    } catch (error) {
      console.error('Error fetching total value:', error);
      throw error;
    }
  }
}

export default CalculatorAPI;
