<script setup>
import Calculator from './components/Calculator.vue'
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.png" width="125" height="*" />
  </header>

  <main>
    <Calculator />
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    z-index: 1000;
    padding: 10px;
  }
}
</style>
