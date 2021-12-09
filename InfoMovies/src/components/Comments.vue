<template>
<Header />
    <h1>Filmo {{this.movie}} komentarų sąrašas</h1>
    <div class="register">
        <button v-if="showIfUser()" v-on:click="createComment()">Sukurkite komentarą</button>
    </div>
    <br>
    <div class="register">
        <button @click="$router.push('Movies')">Grįžti į filmų sąrašą</button>
    </div>
    <br>
    <br>
    <ModalWindow v-show="isModalVisible" @close="closeModal">
        <template v-slot:body>
            <h2>Komentaras</h2>
            <li>Id - {{this.comment.id}}</li>
            <li>Tekstas - {{this.comment.text}}</li>
        </template>
    </ModalWindow>
    <table>
        <td class="firstRow">Tekstas</td>
        <td class="firstRow" v-if="showIfAdmin()"></td>
        <td class="firstRow" v-if="showIfAdmin()"></td>
        <tr v-for="comment in comments" :key="comment.id">
            <td class="highlight" v-on:click="getComment(comment)">{{comment.text}}</td>
            <td class="highlight" v-if="showIfAdmin()" v-on:click="updateComment(comment.id)">Atnaujinti</td>
            <td class="highlight" v-if="showIfAdmin()" v-on:click="deleteComment(comment.id)">Ištrinti</td>
        </tr>   
    </table>
    <p class="footer">InfoMovies</p>
</template>
<script>

import axios from 'axios'
import Header from './Header.vue'
import ModalWindow from "./ModalWindow";
export default{
    name:'Comments',
    data(){
        return {
            name:'',
            movie:'',
            comments:[],
            token:'',
            isModalVisible: false,
            comment:[]
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
        showIfUser(){
            let role = localStorage.getItem("userRole");
            if(role == "SimpleUser" || role == "Admin"){
                return true;
            }
            return false;
        },
        showIfAdmin(){
            let role = localStorage.getItem("userRole");
            if(role == "Admin"){
                return true;
            }
            return false;
        },
        getComment(comment){
            this.comment = comment;
            this.isModalVisible = true;
        },
        createComment(){
            this.$router.push({name: 'CreateComment'});
        },
        updateComment(id){
            localStorage.setItem("chosenComment", id);
            this.$router.push({name: 'UpdateComment'});
        },
        async deleteComment(id){
            let chosenCompany = localStorage.getItem("chosenCompany");
            let companyId = JSON.parse(chosenCompany).id;
            let chosenMovie = localStorage.getItem("chosenMovie");
            let movieId = JSON.parse(chosenMovie).id;
            await axios.delete("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + movieId + "/comments/" + id, { headers: {"Authorization" : `Bearer ${this.token}`} });
            location.reload();
        }
    },

    async mounted(){
        this.token = localStorage.getItem("token");
        let chosenCompany = localStorage.getItem("chosenCompany");
        let companyId = JSON.parse(chosenCompany).id;
        let chosenMovie = localStorage.getItem("chosenMovie");
        let movieId = JSON.parse(chosenMovie).id;
        this.movie = JSON.parse(chosenMovie).title;
        let request =await axios.get("https://infomoviesapi.azurewebsites.net/api/companies/" + companyId + "/movies/" + movieId + "/comments/");
        this.comments=request.data;
    }
}
</script>

