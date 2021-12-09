<template>
<Header />
    <img class="Logo" src="../assets/logo.png">
    <h1>Atnaujinkite duomenis apie filmą</h1>
    <br>
    <br>
    <div class="register">
        <button @click="$router.push('Movies')">Grįžti į filmų sąrašą</button>
    </div>
    <br>
    <br>
    <div class="register">
        <input type="text" v-model="title" placeholder="Įveskite filmo pavadinimą" />
        <input type="text" v-model="story" placeholder="Įveskite filmo aprašymą" />
        <input type="text" v-model="score" placeholder="Įveskite filmo įvertinimą" />
        <button v-on:click="updateMovie()" >Atnaujinti</button>
    </div>
    <p class="footer">InfoMovies</p>
</template>
<script>
import Header from './Header.vue'
import axios from 'axios'
export default{
    name:'UpdateMovie',
    data()
    {
        return {
            title:'',
            story:'',
            score:'',
            token:''
        }
    },
    methods:{
        async updateMovie()
        {
            if(this.title == ''){
                alert("Įveskite filmo pavadinimą");
            }
            else if(this.story == ''){
                alert("Įveskite filmo aprašymą");
            }
            else if(this.score == ''){
                alert("Įveskite filmo įvertinimą");
            }
            this.token=localStorage.getItem("token");
            let chosenCompany = localStorage.getItem("chosenCompany");
            let companyId = JSON.parse(chosenCompany).id;
            let id = localStorage.getItem("chosenMovie");
            let request = await axios.put("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + id,{
                title:this.title,
                story:this.story,
                score:this.score
            }, { headers: {"Authorization" : `Bearer ${this.token}`} }).catch(error => {
                    console.log(error);
                });
            if (request.status == 200){
                this.$router.push({name:'Movies'})
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