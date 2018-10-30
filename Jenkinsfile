pipeline {
  agent {
    node {
      label 'dotnet'
    }

  }
  stages {
    stage('Build the app') {
      steps {
        sh 'dotnet restore'
        sh 'dotnet build'
      }
    }
    stage('Run the app') {
      steps {
        sh 'dotnet run'
      }
    }
  }
}