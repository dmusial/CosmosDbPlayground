pipeline {
  agent {
    node {
      label 'dotnet'
    }

  }
  stages {
    stage('Build the app') {
      steps {
        git(url: 'https://github.com/dmusial/CosmosDbPlayground.git', branch: 'master')
        sh 'dotnet restore'
        sh 'dotnet build'
      }
    }
  }
}