pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/core/sdk:2.1'
      args '--rm -v "$PWD":/src -w /src'
    }

  }
  stages {
    stage('Build') {
      agent any
      steps {
        sh 'ls'
      }
    }

  }
}