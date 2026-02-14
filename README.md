# SkillZone

Projeto em desenvolvimento
Plataforma de conteúdo profissional baseada em assinatura.

# Sobre o projeto

O SkillZone é uma plataforma onde criadores podem publicar conteúdos profissionais como:

- Artigos
- Vídeos
- Materiais complementares

Usuários podem assinar planos para obter acesso ao conteúdo exclusivo, criando um ecossistema sustentável para produtores de conhecimento.

# Objetivo

Construir uma solução escalável para monetização de conteúdo profissional através de:

- Sistema de assinaturas
- Controle de acesso por plano
- Gestão de criadores

# Executando o projeto

### Requisitos

Docker & Docker Compose

### Passo a passo

A aplicação roda 100% containerizada, sem necessidade de instalação local do .NET SDK.

**Subir containers**

`docker compose up -d --build`

**Aplicar migrations**

`docker exec -it skillzone-api dotnet ef database update`
