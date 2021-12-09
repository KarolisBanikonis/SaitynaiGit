<template>
  <Header />
  <div>
    <h1>InfoMovies</h1>
    <p>InfoMovies puslapis padeda Jums sužinoti daugiau apie populiuriausius filmus,
      juos kuriančias kompanijas, bei kitų lankytojų nuomonę.
    </p>
    <div class="register">
      <button @click="$router.push('companies')">Kompanijų sąrašas</button>
    </div>
  </div>
  <br><br>
  <img class="img" src="../assets/movies2.jpg">
  <img class="img" src="../assets/movies2.jpg">
  <p class="footer">InfoMovies</p>
</template>

<script>
import Header from './Header.vue'
export default {
  name: 'Home',
  components:{
        Header
  }
}
</script>