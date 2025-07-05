### CSharpCommonLibrary 소개
CSharpCommonLibrary는 .NET Framework 4.5 기반의 다양한 공통 유틸리티, 확장 메서드, 시스템/환경 도우미, 파일/이미지/스크린 관리, 로깅, 리플렉션, Enum/Attribute/문자열/수학 유틸리티 등을 제공하는 라이브러리입니다.

### 주요 클래스/열거형 설명 (네임스페이스별)

#### CommonLibrary
- **AspectRatio**: 정수형 화면/이미지 비율을 계산하고 표현하는 클래스
- **AspectRatioF**: 실수형 화면/이미지 비율을 계산하고 표현하는 클래스
- **CertFile**: 인증서 파일을 시스템에 추가/관리하는 기능 제공
- **ColorData**: Color, int ARGB, Hex 문자열 간 변환 및 색상 정보(알파, ARGB, Hex 등) 제공
- **CertFileStoreStatus (enum)**: 인증서 파일 저장 상태를 나타내는 열거형
- **Compare (enum)**: 비교 연산(크다, 작다, 같다)을 나타내는 열거형
- **Explorer**: Windows explorer 프로세스 관련 상수 제공
- **ExternalLoggingEventArgs**: 외부 로깅 이벤트 정보를 담는 클래스
- **LogLevel**: 다양한 로깅 레벨(ALL, TRACE, DEBUG, INFO, WARN, ERROR, FATAL, OFF) 관리
- **ScreenManager**: 멀티 모니터 환경에서 스크린 정보 및 인덱스 관리
- **ShowScreenIndex (enum)**: 스크린 인덱스 다이얼로그 표시 모드 구분
- **StringResourceAttribute**: 문자열 리소스(포맷/메시지)용 커스텀 Attribute

#### CommonLibrary.IO
- **FileCreateInfo**: 파일 생성 시 옵션(잠금, 모드, 접근권한) 정보를 담는 클래스
- **ImageFileCreateInfo**: 이미지 파일 생성 정보(디렉토리, 확장자, 파일 생성 옵션 등) 제공
- **ImageManager**: 디렉토리 내 이미지 파일을 읽어 Image 리스트로 반환하는 기능 제공

#### CommonLibrary.Reflection
- **AssemblyBuildDateAttribute**: 어셈블리 빌드 날짜 정보를 저장하는 Attribute
- **AssemblyBuildInfo**: 어셈블리에서 빌드 날짜 정보를 추출하는 유틸리티

#### CommonLibrary.Extensions
- **AttributeExtension**: Attribute 관련 확장 메서드 제공
- **EnumExtension**: Enum 관련 확장 메서드 제공
- **ScreenExtension**: System.Windows.Forms.Screen 관련 확장 메서드 제공

#### CommonLibrary.Extensions.UI
- **RichTextBoxExtension**: RichTextBox에 컬러 텍스트 추가 등 확장 메서드 제공

#### CommonLibrary.UI
- **ScreenIndexDialog**: 멀티 모니터 환경에서 스크린 인덱스 정보를 시각적으로 표시하는 폼
- **ScreenIndexDialogColor**: ScreenIndexDialog의 각 UI 요소별 색상 정보 제공

#### CommonLibrary.Utilities
- **ColorUtility**: Color, int ARGB, Hex 문자열 간 상호 변환 및 색상 관련 유틸리티 제공
- **ConverUtility**: Base64, 바이트/문자열 변환, 단위 변환, 스트림/이미지 변환 등 다양한 변환 유틸리티 제공
- **ImageUtility**: 이미지 리사이즈, Base64 변환 등 이미지 관련 유틸리티 제공
- **RegexUtility**: 컬러, 알파벳, 이메일 등 다양한 정규식 기반 유효성 검사 유틸리티 제공
- **EnvironmentUtility**: 시스템 환경, 폴더 경로 등 환경 관련 유틸리티 제공
- **MathUtility**: 수학적 계산(최대/최소/최대공약수/좌표변환 등) 유틸리티 제공
- **StringUtility**: 문자열 관련 유틸리티(대소문자 변환, 알파벳 판별 등) 제공

### Coding Convention   
해당 저장소의 코딩 표준 가이드는 아래 링크의 포프님 C# 코딩 표준 을 따릅니다.   
https://docs.popekim.com/ko/coding-standards/csharp

클래스 기능에 대한 설명 Copilot 에이전트를 통해 생성합니다.