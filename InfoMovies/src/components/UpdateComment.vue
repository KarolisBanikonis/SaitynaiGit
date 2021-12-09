<template>
<Header />
    <img class="Logo" src="../assets/logo.png">
    <h1>Atnaujinkite duomenis apie komentarą</h1>
    <br>
    <br>
    <div class="register">
        <button @click="$router.push('Comments')">Grįžti į komentarų sąrašą</button>
    </div>
    <br>
    <br>
    <div class="register">
        <input type="text" v-model="text" placeholder="Įveskite komentarą" />
        <button v-on:click="updateComment()" >Atnaujinti</button>
    </div>
    <p class="footer">InfoMovies</p>
</template>
<script>
import Header from './Header.vue'
import axios from 'axios'
export default{
    name:'UpdateComment',
    data()
    {
        return {
            text:'',
            token:''
        }
    },
    methods:{
        async updateComment()
        {
            if(this.text == ''){
                alert("Įveskite komentarą");
            }
            this.token=localStorage.getItem("token");
            let chosenCompany = localStorage.getItem("chosenCompany");
            let companyId = JSON.parse(chosenCompany).id;
            let chosenMovie = localStorage.getItem("chosenMovie");
            let movieId = JSON.parse(chosenMovie).id;
            let id = localStorage.getItem("chosenComment");
            let request = await axios.put("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + movieId + "/comments/" + id,{
                text:this.text
            }, { headers: {"Authorization" : `Bearer ${this.token}`} }).catch(error => {
                    console.log(error);
                });
            if (request.status == 200){
                this.$router.push({name:'Comments'})
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