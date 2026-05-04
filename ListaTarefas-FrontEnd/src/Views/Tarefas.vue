<!-- Tarefas.vue — Pai -->
<script setup>
import { ref, onMounted, watch } from "vue";
import Botoes from "../Componentes/botoes.vue";
import HeaderTodo from "../Componentes/HeaderTodo.vue";
import ListaSuspensa from "../Componentes/ListaSuspensa.vue";
import CriarModal from "../Componentes/Modals/CriarModal.vue";
import EditarModal from "../Componentes/Modals/EditarModal.vue";
import Swal from "sweetalert2";
import { notify } from "@kyvg/vue3-notification";

const tarefas = ref([]);
const filtro = ref({ textoBusca: "", status: "" });

const mostrarCriar = ref(false);
const mostrarEditar = ref(false);
const tarefaSelecionada = ref(null);

const loading = ref(false);

const API = "https://localhost:7265/api/Tarefas";

async function setFiltro(filtroProp) {
	filtro.value = filtroProp;
	buscarTarefas();
}

async function buscarTarefas() {
	try {
		loading.value = true;
		await new Promise((resolve) => setTimeout(resolve, 500)); // é para visualizar a rodela
		const params = new URLSearchParams();
		if (filtro.value.textoBusca)
			params.append("search", filtro.value.textoBusca);

		if (filtro.value.status !== "")
			params.append("status", filtro.value.status);

		const url = params.toString() ? `${API}?${params}` : API;

		const res = await fetch(url);
		const data = await res.json();
		if (!res.ok)
			throw new Error(data.message || data.erro || "Erro desconhecido");
		tarefas.value = data;
	} catch (e) {
		notify({ type: "error", text: e.message });
	} finally {
		loading.value = false;
	}
}

async function criarTarefa(nova) {
	try {
		const res = await fetch(API, {
			method: "POST",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify(nova),
		});
		if (!res.ok) {
			const data = await res.json();
			const mensagem =
				data.message ||
				data.erro ||
				data.title ||
				Object.values(data.errors ?? {})[0]?.[0] ||
				"Erro desconhecido";
			throw new Error(mensagem);
		}
		await buscarTarefas();
		notify({ type: "success", text: "Tarefa criada com sucesso!" });
	} catch (e) {
		notify({ type: "error", text: e.message });
	}
}
async function salvarEdicao(editada) {
	try {
		const res = await fetch(`${API}/${editada.id}`, {
			method: "PUT",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify({
				titulo: editada.titulo,
				descricao: editada.descricao,
				status: Number(editada.status),
			}),
		});
		if (!res.ok) {
			const data = await res.json();
			const mensagem =
				data.message ||
				data.erro ||
				data.title ||
				Object.values(data.errors ?? {})[0]?.[0] ||
				"Erro desconhecido";
			throw new Error(mensagem);
		}
		await buscarTarefas();
		notify({ type: "success", text: "Tarefa editada com sucesso!" });
	} catch (e) {
		notify({ type: "error", text: e.message });
	}
}

async function deletarTarefa(id) {
	const { isConfirmed } = await Swal.fire({
		title: "Tem certeza?",
		text: "Essa ação não pode ser desfeita.",
		icon: "warning",
		showCancelButton: true,
		confirmButtonText: "Sim, deletar",
		cancelButtonText: "Cancelar",
		background: "var(--gray-500)",
		color: "var(--gray-100)",
		confirmButtonColor: "#ef4444",
	});
	if (!isConfirmed) return;

	try {
		const res = await fetch(`${API}/${id}`, { method: "DELETE" });
		if (!res.ok) {
			const data = await res.json();
			throw new Error(data.message || data.erro || "Erro desconhecido");
		}
		await buscarTarefas();
		notify({ type: "success", text: "Foi deletado com sucesso!" });
	} catch (e) {
		notify({ type: "error", text: e.message });
	}
}
function abrirEditar(tarefa) {
	tarefaSelecionada.value = tarefa;
	mostrarEditar.value = true;
}

onMounted(buscarTarefas);
</script>

<template>
	<div class="app-container">
		<HeaderTodo />

		<Botoes
			:filtro="filtro"
			@atualizar:filtro="setFiltro"
			@abrir-criar="mostrarCriar = true"
		/>

		<ListaSuspensa
			v-if="!loading"
			:tarefas="tarefas"
			class="lista-expand"
			@editar="abrirEditar"
			@deletar="deletarTarefa"
		/>

		<div v-else class="loading">
			<div class="spinner"></div>
			<span>Carregando...</span>
		</div>

		<CriarModal
			v-if="mostrarCriar"
			@fechar="mostrarCriar = false"
			@salvar="criarTarefa"
		/>

		<EditarModal
			v-if="mostrarEditar"
			:tarefa="tarefaSelecionada"
			@fechar="mostrarEditar = false"
			@salvar="salvarEdicao"
		/>
	</div>
</template>

<style scoped>
.app-container {
	display: flex;
	flex-direction: column;
	min-height: 100vh;
}

.lista-expand {
	flex: 1;
}

.loading {
	flex: 1;
	display: flex;
	flex-direction: column;
	align-items: center;
	justify-content: center;
	gap: 16px;
	background-color: var(--gray-600);
}

.spinner {
	width: 48px;
	height: 48px;
	border-radius: 50%;
	border: 3px solid var(--gray-400);
	border-top-color: var(--purple);
	animation: spin 0.8s linear infinite;
}

.loading span {
	font-size: 0.875rem;
	font-weight: 600;
	color: var(--gray-300);
}

@keyframes spin {
	to {
		transform: rotate(360deg);
	}
}
</style>
