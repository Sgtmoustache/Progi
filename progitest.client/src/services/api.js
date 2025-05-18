class CalculatorAPI {
  constructor() {
    this.baseURL = 'https://localhost:7210';
  }

  async getCalculationResult(carPrice, type) {
    try {
      const response = await fetch(`${this.baseURL}/Calculator`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({carPrice: carPrice, carType: type}),
      });

      if (!response.ok) {
        throw new Error('Network response was not ok ' + response.statusText);
      }
      return await response.json();
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
      return await response.json();
    } catch (error) {
      console.error('Error fetching total value:', error);
      throw error;
    }
  }
}

export default CalculatorAPI;
