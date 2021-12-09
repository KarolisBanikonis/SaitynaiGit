<template>
<Header />
    <h1>Kompanijos {{this.company}} filmų sąrašas</h1>
    <div class="register">
        <button v-if="showIfAdmin()" v-on:click="createMovie()">Sukurkite filmą</button>
    </div>
    <br>
    <div class="register">
        <button @click="$router.push('companies')">Grįžti į kompanijų sąrašą</button>
    </div>
    <br>
    <br>
    <ModalWindow v-show="isModalVisible" @close="closeModal">
        <template v-slot:body>
            <h2>Filmas</h2>
            <li>Id - {{this.movie.id}}</li>
            <li>Pavadinimas - {{this.movie.title}}</li>
            <li>Aprašymas - {{this.movie.story}}</li>
            <li>Įvertinimas - {{this.movie.score}}</li>
        </template>
    </ModalWindow>
    <table>
        <td class="firstRow">Pavadinimas</td>
        <td class="firstRow">Aprašymas</td>
        <td class="firstRow">Įvertinimas</td>
        <td class="firstRow"></td>
        <td  class="firstRow" v-if="showIfAdmin()"></td>
        <td  class="firstRow" v-if="showIfAdmin()"></td>
        <tr v-for="movie in movies" :key="movie.id">
            <td class="highlight" v-on:click="getMovie(movie)">{{movie.title}}</td>
            <td class="highlight" v-on:click="getMovie(movie)">{{movie.story}}</td>
            <td class="highlight" v-on:click="getMovie(movie)">{{movie.score}}</td>
            <td class="highlight" v-on:click="getComments(movie.id)" >Komentarai</td>
            <td class="highlight" v-if="showIfAdmin()" v-on:click="updateMovie(movie.id)">Atnaujinti</td>
            <td class="highlight" v-if="showIfAdmin()" v-on:click="deleteMovie(movie.id)">Ištrinti</td>
        </tr>   
    </table>
    <p class="footer">InfoMovies</p>
</template>
<script>

import axios from 'axios'
import Header from './Header.vue'
import ModalWindow from "./ModalWindow";
export default{
    name:'Movies',
    data(){
        return {
            company:'',
            movies:[],
            token :'',
            isModalVisible: false,
            movie:[]
        }
    },
    components:{
        Header,
        ModalWindow
    },
    methods:{
        closeModal() {
            this.isModalVisible = false;
        },
        showIfAdmin(){
            let role = localStorage.getItem("userRole");
            if(role == "Admin"){
                return true;
            }
            return false;
        },
        getMovie(movie){
            this.movie = movie;
            this.isModalVisible = true;
        },
        createMovie(){
            this.$router.push({name: 'CreateMovie'});
        },
        updateMovie(id){
            localStorage.setItem("chosenMovie", id);
            this.$router.push({name: 'UpdateMovie'});
        },
        async deleteMovie(id){
            let chosenCompany = localStorage.getItem("chosenCompany");
            let companyId = JSON.parse(chosenCompany).id;
            await axios.delete("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + id, { headers: {"Authorization" : `Bearer ${this.token}`} });
            location.reload();
        },
        async getComments(id){
            let chosenCompany = localStorage.getItem("chosenCompany");
                let companyId = JSON.parse(chosenCompany).id;
                let request =await axios.get("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + id);
                localStorage.setItem("chosenMovie", JSON.stringify(request.data));
                this.$router.push({name:'Comments'});
        }
    },

    async mounted(){
        this.token = localStorage.getItem("token");
        let chosenCompany = localStorage.getItem("chosenCompany");
        let companyId = JSON.parse(chosenCompany).id;
        this.company = JSON.parse(chosenCompany).name;
        let request =await axios.get("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/");
        this.movies=request.data;
    }
}
</script>
