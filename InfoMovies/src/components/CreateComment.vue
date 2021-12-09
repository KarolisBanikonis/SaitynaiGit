<template>
<Header />
    <img class="Logo" src="../assets/logo.png">
    <h1>Sukurkite komentarą</h1>
    <br>
    <br>
    <div class="register">
        <button @click="$router.push('Comments')">Grįžti į komentarų sąrašą</button>
    </div>
    <br>
    <br>
    <div class="register">
        <input type="text" v-model="text" placeholder="Įveskite komentarą" />
        <button v-on:click="createComment()" >Sukurti</button>
    </div>
    <p class="footer">InfoMovies</p>
</template>
<script>
import Header from './Header.vue'
import axios from 'axios'
export default{
    name:'CreateComment',
    data()
    {
        return {
            text:'',
            token:''
        }
    },
    methods:{
        async createComment()
        {
            if(this.text == ''){
                alert("Įveskite komentarą");
            }
            this.token=localStorage.getItem("token");
            let chosenCompany = localStorage.getItem("chosenCompany");
            let companyId = JSON.parse(chosenCompany).id;
            let chosenMovie = localStorage.getItem("chosenMovie");
            let movieId = JSON.parse(chosenMovie).id;
            let request = await axios.post("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + movieId + "/comments/",{
                text:this.text
            }, { headers: {"Authorization" : `Bearer ${this.token}`} }).catch(error => {
                    console.log(error);
                });
            if (request.status == 201){
                this.$router.push({name:'Comments'})
            }
        }
    },
    mounted(){
        let role = localStorage.getItem("userRole");
        if(role == "SimpleUser"){
            console.warn(role);
        }
        else if(role == "Admin"){
            console.warn(role);
        }
        else{
            this.$router.push({name: 'Home'});
        }
    },
    components:{
        Header
    }
}
</script>