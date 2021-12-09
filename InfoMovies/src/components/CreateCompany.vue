<template>
<Header />
    <img class="Logo" src="../assets/logo.png">
    <h1>Sukurkite kompaniją</h1>
    <div class="register">
        <button @click="$router.push('Companies')">Grįžti į kompanijų sąrašą</button>
    </div>
    <br>
    <br>
    <div class="register">
        <input type="text" v-model="name" placeholder="Įveskite kompanijos pavadinimą" />
        <input type="text" v-model="description" placeholder="Įveskite kompanijos aprašymą" />
        <button v-on:click="createCompany()" >Sukurti</button>
    </div>
    <p class="footer">InfoMovies</p>
</template>
<script>
import Header from './Header.vue'
import axios from 'axios'
export default{
    name:'CreateCompany',
    data()
    {
        return {
            name:'',
            description:'',
            token:''
        }
    },
    methods:{
        async createCompany()
        {
            if(this.name == ''){
                alert("Įveskite kompanijos pavadinimą");
            }
            else if(this.description == ''){
                alert("Įveskite kompanijos aprašymą");
            }
            this.token=localStorage.getItem("token");
            let request = await axios.post("https://infomoviesapi.azurewebsites.net/api/companies",{
                name:this.name,
                description:this.description
            }, { headers: {"Authorization" : `Bearer ${this.token}`} }).catch(error => {
                    console.log(error);
                });
            if (request.status == 201){
                this.$router.push({name:'Companies'})
            }
        }
    },
    mounted(){
        let role = localStorage.getItem("userRole");
        if(role != "Admin"){
            this.$router.push({name: 'Companies'});
        }
    },
    components:{
        Header
    }
}
</script>